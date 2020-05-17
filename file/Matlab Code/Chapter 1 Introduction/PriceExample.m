% Obtaining the price of the Heston call or put

clc; clear;

% Option features
S = 100;         % Spot price
K = 100;         % Strike price
tau = .5;        % Maturity
r = 0.03;        % Risk free rate
q = 0.0;        % Dividend yield
kappa = 5;       % Heston parameter : reversion speed
sigma = 0.5;     % Heston parameter : volatility of variance
rho   = -0.8;    % Heston parameter : correlation
theta = 0.05;    % Heston parameter : reversion level
v0    = 0.05;    % Heston parameter : initial variance
lambda = 0;      % Heston parameter : risk preference

% Expression for the characteristic function
Trap = 0;        % 0 = Original Heston formulation
                 % 1 = Albrecher et al formulation

% Integration range				 
Lphi = 0.000001;  % Lower limit
dphi = 0.01;      % Increment
Uphi = 50;        % Upper limit

% Obtain the Heston put and call
HPut  = HestonPrice('P',kappa,theta,lambda,rho,sigma,tau,K,S,r,q,v0,Trap,Lphi,Uphi,dphi);
HCall = HestonPrice('C',kappa,theta,lambda,rho,sigma,tau,K,S,r,q,v0,Trap,Lphi,Uphi,dphi);

% Output the result
disp(['The Heston Put is  ' num2str(HPut) ])
disp(['The Heston Call is ' num2str(HCall) ])
