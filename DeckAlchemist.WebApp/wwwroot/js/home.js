$(document).ready(function () {
    $('#table').bootstrapTable({
        columns: [{
            field: 'id',
            title: '#',
            align: 'center',
            halign: 'center'
        }, {
            field: 'name',
            title: 'Name',
            align: 'center',
            halign: 'center'
        }, {
            field: 'ccost',
            title: 'Converted Cost',
            align: 'center',
            halign: 'center'
        }, {
            field: 'fcost',
            title: 'Full Cost',
            align: 'center',
            halign: 'center'
        }, {
            field: 'color',
            title: 'Color',
            align: 'center',
            halign: 'center'
        }, {
            field: 'power',
            title: 'Power',
            align: 'center',
            halign: 'center'
        }, {
            field: 'toughness',
            title: 'Toughness',
            align: 'center',
            halign: 'center'
        }, {
            field: 'type',
            title: 'Type',
            align: 'center',
            halign: 'center'
        }, {
            field: 'set',
            title: 'Set',
            class: 'set-style',
            align: 'center',
            halign: 'center'
        }, {
            field: 'rarity',
            title: 'Rarity',
            align: 'center',
            halign: 'center'
        }, {
            field: 'avalibility',
            title: 'Avalibility',
            align: 'center',
            halign: 'center'
        }],
        data: [{
            id: 1,
            name: 'Test',
            ccost: 4,
            fcost: 5,
            color: 'U',
            power: NaN,
            toughness: NaN,
            type: 'Instant',
            set: 'The best set',
            rarity: 'U',
            avalibility: 'Y'
        }, {
            id: 1,
            name: 'Test',
            ccost: 4,
            fcost: 5,
            color: 'U',
            power: NaN,
            toughness: NaN,
            type: 'Instant',
            set: 'The best set',
            rarity: 'U',
            avalibility: 'Y'
        }, {
            id: 1,
            name: 'Test',
            ccost: 4,
            fcost: 5,
            color: 'U',
            power: NaN,
            toughness: NaN,
            type: 'Instant',
            set: 'The best set',
            rarity: 'U',
            avalibility: 'Y'
        }, {
            id: 1,
            name: 'Test',
            ccost: 4,
            fcost: 5,
            color: 'U',
            power: NaN,
            toughness: NaN,
            type: 'Instant',
            set: 'The best set',
            rarity: 'U',
            avalibility: 'Y'
        }, {
            id: 1,
            name: 'Test',
            ccost: 4,
            fcost: 5,
            color: 'U',
            power: NaN,
            toughness: NaN,
            type: 'Instant',
            set: 'The best set',
            rarity: 'U',
            avalibility: 'Y'
        }, {
            id: 1,
            name: 'Test',
            ccost: 4,
            fcost: 5,
            color: 'U',
            power: NaN,
            toughness: NaN,
            type: 'Instant',
            set: 'The best set',
            rarity: 'U',
            avalibility: 'Y'
        }, {
            id: 1,
            name: 'Test',
            ccost: 4,
            fcost: 5,
            color: 'U',
            power: NaN,
            toughness: NaN,
            type: 'Instant',
            set: 'The best set',
            rarity: 'U',
            avalibility: 'Y'
        }, {
            id: 1,
            name: 'Test',
            ccost: 4,
            fcost: 5,
            color: 'U',
            power: NaN,
            toughness: NaN,
            type: 'Instant',
            set: 'The best set',
            rarity: 'U',
            avalibility: 'Y'
        }, {
            id: 1,
            name: 'Test',
            ccost: 4,
            fcost: 5,
            color: 'U',
            power: NaN,
            toughness: NaN,
            type: 'Instant',
            set: 'The best set',
            rarity: 'U',
            avalibility: 'Y'
        }]
    });

    $(window).resize(function () {
        $('#table').bootstrapTable('resetView');
    });
});