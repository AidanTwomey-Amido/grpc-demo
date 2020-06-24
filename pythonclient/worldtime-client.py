from __future__ import print_function
import logging

import grpc

import worldtime_pb2
import worldtime_pb2_grpc


def run():
    with grpc.insecure_channel('localhost:8001') as channel:
        stub = worldtime_pb2_grpc.WorldTimeStub(channel)
        response = stub.GetTime(worldtime_pb2.Point(latitude = 38.89511, longitude = -77.03637 ))
    print("Greeter client received: " + response.localtime)


if __name__ == '__main__':
    logging.basicConfig()
    run()
