<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Coupon extends Model
{
    use PrismaDates;

    protected $table = 'Coupon';

    protected $primaryKey = 'code';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'code',
        'userPropertiesToUpdate',
        'dateRedeemed',
    ];

    protected $casts = [
        'userPropertiesToUpdate' => 'array',
        'dateRedeemed' => 'datetime',
    ];
}