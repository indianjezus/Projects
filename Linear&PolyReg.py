import pandas as pd
import numpy as np

data = np.genfromtxt('LR.csv', delimiter = ',')

x = data[:, 0]
y = data[:, 3]



#linear regression

def slpint(x, y):
    
    meanx = np.mean(x)
    meany = np.mean(y)
    sdx = np.std(x)
    sdy = np.std(y)
    
    covxy = np.cov(x,y)[1][0]
    
    slope = covxy/(sdx**2)
    intercept = meany - slope * meanx
    
    return slope, intercept

print("slope and intercept are: ")
slope, intercept = slpint(x,y)
print(slope, intercept)
print(" ")

mse = []

K = 10
n = len(data)

for i in range(K):
    
    xtest = x[i*(n//K):i*(n//K)+(n//K)]
    ytest = y[i*(n//K):i*(n//K)+(n//K)]
    
    xtrain = x[(i*(n//K) <= np.arange(len(x))) & (np.arange(len(x)) < i*(n//K)+(n//K))]

    ytrain = y[(i*(n//K) <= np.arange(len(y))) & (np.arange(len(y)) < i*(n//K)+(n//K))]

    
    slp, inter = slpint(xtrain,ytrain)
    
    yp = slp*xtest + inter
    
    mses = np.mean((yp - ytest)**2)
    mse.append(mses)

generalization_error = np.mean(mse)
print("generalization_error is: ")
print(generalization_error)
print(" ")








#polynomial regrsssion

d2 = np.polyfit(x,y,2)
d3 = np.polyfit(x,y,3)
d4 = np.polyfit(x,y,4)
d5 = np.polyfit(x,y,5)

def polyeval(frame):

    for i in range(3):
        x = 1
        z = 0
        for p in frame:
            z = z + p*pow(i+1, len(frame) - x)
            x = x + 1
        print("Value is for p =", i, "is: ")
        print(z)
        print(" ")
        
        
def error(z):
    
    K = 10
    n = len(x)
    msep = []
    
    
    for i in range(K):
    
        xtest = x[i*(n//K):i*(n//K)+(n//K)]
        ytest = y[i*(n//K):i*(n//K)+(n//K)]
        
        xtrain = x[(i*(n//K) <= np.arange(len(x))) & (np.arange(len(x)) < i*(n//K)+(n//K))]
    
        ytrain = y[(i*(n//K) <= np.arange(len(y))) & (np.arange(len(y)) < i*(n//K)+(n//K))]
        
        
        poly = np.polyfit(xtrain,ytrain,z)
        
        yp = np.polyval(poly, xtest)
        
        msesp = np.mean((yp - ytest)**2)
        msep.append(msesp)
        
    return np.mean(msep)
    


print(d2)
print(d3)
print(d4)
print(d5)
print(" ")

polyeval(d2)
polyeval(d3)
polyeval(d4)
polyeval(d5)

print("error for degree 2 is", error(2))
print("error for degree 3 is", error(3))
print("error for degree 4 is", error(4))
print("error for degree 5 is", error(5))







#multilinear regression

x1 = data[:, 0]
x2 = data[:, 1]
x3 = data[:, 2]

x = np.column_stack((x1, x2, x3, np.ones(len(x1))))
b = np.linalg.inv(x.T.dot(x)).dot(x.T).dot(y)

print("The equation is", b[0], "*x1 +", b[1], "*x2 +", b[2])


vals = [(1, 1, 1), (2, 0, 4), (3, 2, 1)]
for v in vals:
    prediction = b[0]*v[0] + b[1]*v[1] + b[2]*v[2] + b[3]
    print("Prediction for", v, "=", prediction)


msem = []
for i in range(K):
    
    
    x1test = x1[i*(n//K):i*(n//K)+(n//K)]
    x2test = x2[i*(n//K):i*(n//K)+(n//K)]
    x3test = x3[i*(n//K):i*(n//K)+(n//K)]
    
    ytest = y[i*(n//K):i*(n//K)+(n//K)]
    
    
    x1train = x1[(i*(n//K) <= np.arange(len(x1))) & (np.arange(len(x1)) < i*(n//K)+(n//K))]
    x2train = x2[(i*(n//K) <= np.arange(len(x2))) & (np.arange(len(x2)) < i*(n//K)+(n//K))]
    x3train = x3[(i*(n//K) <= np.arange(len(x3))) & (np.arange(len(x3)) < i*(n//K)+(n//K))]
    
    ytrain = y[(i*(n//K) <= np.arange(len(y))) & (np.arange(len(y)) < i*(n//K)+(n//K))]
    
    
    xm_train = np.column_stack((x1train, x2train, x3train, np.ones(len(x1train))))
    
    slpt = np.linalg.inv(xm_train.T.dot(xm_train)).dot(xm_train.T).dot(ytrain) 
    
    
    yp = slpt[0]*x1test + slpt[1]*x2test + slpt[2]*x3test + slpt[3]
    msem.append(np.mean((yp - ytest)**2))


ge = np.mean(msem)
print("generalization error: ", ge)
