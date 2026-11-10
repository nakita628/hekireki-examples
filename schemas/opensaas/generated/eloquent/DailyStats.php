<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class DailyStats extends Model
{
    use PrismaDates;

    protected $table = 'DailyStats';

    public $timestamps = false;

    protected $fillable = [
        'date',
        'totalViews',
        'prevDayViewsChangePercent',
        'userCount',
        'paidUserCount',
        'userDelta',
        'paidUserDelta',
        'totalRevenue',
        'totalProfit',
    ];

    protected $attributes = [
        'totalViews' => 0,
        'prevDayViewsChangePercent' => '0',
        'userCount' => 0,
        'paidUserCount' => 0,
        'userDelta' => 0,
        'paidUserDelta' => 0,
        'totalRevenue' => 0.0,
        'totalProfit' => 0.0,
    ];

    protected $casts = [
        'date' => 'datetime',
        'totalViews' => 'integer',
        'userCount' => 'integer',
        'paidUserCount' => 'integer',
        'userDelta' => 'integer',
        'paidUserDelta' => 'integer',
        'totalRevenue' => 'float',
        'totalProfit' => 'float',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('date', $this->attributes)) {
                $this->setAttribute('date', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }

    public function sources(): HasMany
    {
        return $this->hasMany(PageViewSource::class, 'dailyStatsId');
    }
}