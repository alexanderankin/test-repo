for loop in range(0, 100):
    # data setup
    instances = []  # noqa

    # rendered data
    instances.append(('i1', 100))
    instances.append(('i2', 100))
    instances.append(('i3', 20))

    # selecting a backend
    backend = None

    total = sum(i[1] for i in instances)

    import random  # noqa

    random = random.randint(0, total)

    counter = 0
    for i in instances:
        counter += i[1]
        if counter >= random:
            backend = i[0]
            break
    if backend is None:
        backend = instances[len(instances) - 1][0]

    print(f'random {random} gave backend {backend}')
