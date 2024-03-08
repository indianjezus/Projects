import yfinance as yf
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd 
import tensorflow as tf
from sklearn.preprocessing import MinMaxScaler
from keras.models import Sequential
from keras.layers import LSTM
from keras.layers import Dropout
from keras.layers import Dense 

#dataset_train = pd.read_csv('AAPL.csv')

#print(dataset_train)

traingTicker = 'AAPL'
testingTicker = 'GOOGL'

def getData(ticker, period):
    tickers = yf.Tickers(ticker)
    data = tickers.tickers[ticker].history(period=period)
    data.reset_index(inplace=True)
    data['Date'] = data['Date'].dt.strftime('%Y-%m-%d')
    data.drop(['Dividends','Stock Splits'], inplace=True, axis=1)
    data.to_dict(orient='records')
    return data

stock_data = getData('AAPL', '1y')
stock_prices = stock_data.iloc[:, 1:2].values

scaler = MinMaxScaler(feature_range=(0,1))
scaled_prices = scaler.fit_transform(stock_prices)
print(scaled_prices.shape)

input_sequences = []
output_prices = []
for i in range(60, scaled_prices.shape[0]):
    input_sequences.append(scaled_prices[i-60:i, 0])
    output_prices.append(scaled_prices[i, 0])
input_sequences, output_prices = np.array(input_sequences), np.array(output_prices)

input_sequences = np.reshape(input_sequences, (input_sequences.shape[0], input_sequences.shape[1], 1))

    
units = 256
drop = .2

model = Sequential()

model.add(LSTM(units=units,return_sequences=True,input_shape=(input_sequences.shape[1], 1)))
model.add(Dropout(drop))
model.add(LSTM(units=units,return_sequences=True))
model.add(Dropout(drop))
model.add(LSTM(units=units,return_sequences=True))
model.add(Dropout(drop))
model.add(LSTM(units=units))
model.add(Dropout(drop))
model.add(Dense(units=1))
model.compile(optimizer='adam',loss='mean_squared_error')


tickers = ['AAPL','AMZN','INTC','META','MSFT','ORCL','NVDA']
history = []

for t in tickers:
    dataset_train = getData(t, '1y')
    training_set = dataset_train.iloc[:, 1:2].values

    print(dataset_train)

    sc = MinMaxScaler(feature_range=(0,1))
    training_set_scaled = sc.fit_transform(training_set)
    print(training_set_scaled.shape)

    X_train = []
    y_train = []
    for i in range(60, training_set_scaled.shape[0]):
        X_train.append(training_set_scaled[i-60:i, 0])
        y_train.append(training_set_scaled[i, 0])
    X_train, y_train = np.array(X_train), np.array(y_train)

    X_train = np.reshape(X_train, (X_train.shape[0], X_train.shape[1], 1))

    h = model.fit(X_train,y_train,epochs=100,batch_size=None)
    history.append(h)



model.save('LSTMmodel.h5')


dataset_test = getData('GOOGL', 'YTD')

real_stock_price = dataset_test.iloc[:, 1:2].values

dataset_total = pd.concat((dataset_train['Open'], dataset_test['Open']), axis = 0)
inputs = dataset_total[len(dataset_total) - len(dataset_test) - 60:].values
inputs = inputs.reshape(-1,1)
inputs = sc.transform(inputs)
X_test = []
print(inputs.shape)
for i in range(60, inputs.shape[0]):
    X_test.append(inputs[i-60:i, 0])
X_test = np.array(X_test)
X_test = np.reshape(X_test, (X_test.shape[0], X_test.shape[1], 1))
predicted_stock_price = model.predict(X_test)

predicted_stock_price = sc.inverse_transform(predicted_stock_price)

plt.figure(1)
plt.plot(real_stock_price, color = 'black', label = 'GOOGL Stock Price')
plt.plot(predicted_stock_price, color = 'red', label = 'Predicted GOOGL Stock Price')
plt.title('GOOGL YTD Stock Price Prediction')
plt.xlabel('Days Since 1/1/23')
plt.ylabel('GOOGL Stock Price')
plt.legend()
plt.savefig('Google YTD price prediction trained from various tech stocks')
'''
plt.figure(2)
plt.plot(history.history['loss'])
plt.title('model loss')
plt.ylabel('loss')
plt.xlabel('epoch')
plt.yscale('log')
plt.legend(['train'], loc='upper right')
plt.savefig('model1Loss')
'''