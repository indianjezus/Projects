'''
Author: Vihan Garg
University of Wyoming COSC 4555/5555 Machine Learning, Spring 2023
-------

'''

import numpy as np


def split_into_train_and_test(x_all_LF, frac_test=0.5, random_state=None):

    
    if random_state is None:
        random_state = np.random
    
    
    xl = x_all_LF.copy()
    
    L = xl[1].size * frac_test
    
    if L.is_integer() == False :
        L = round(xl[1].size * frac_test + .5)
    else :
        L = int(L)
    
    random_state.shuffle(xl)
    
    n_train = xl[:L] 
    n_test = xl[L:xl[1].size] 
    
    
    return n_train, n_test
    
    
    
x_LF = np.eye(10)
xcopy_LF = x_LF.copy() # preserve what input was before the call
train_MF, test_NF = split_into_train_and_test(
x_LF, frac_test=0.201, random_state=np.random.RandomState(0))
train_MF.shape
test_NF.shape
print(train_MF)
print(test_NF)
print(np.allclose(x_LF, xcopy_LF))

print(x_LF[1].size)

print(x_LF[:x_LF[1].size])


