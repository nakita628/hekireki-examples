<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Team extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at', 'deleted_at'];

    protected $table = 'team';

    protected $primaryKey = 'team_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'team_id',
        'name',
        'access_code',
        'logo_url',
        'two_factor_required',
        'deleted_at',
    ];

    protected $attributes = [
        'two_factor_required' => false,
    ];

    protected $casts = [
        'two_factor_required' => 'boolean',
        'deleted_at' => 'datetime',
    ];

    public function websites(): HasMany
    {
        return $this->hasMany(Website::class, 'team_id');
    }

    public function members(): HasMany
    {
        return $this->hasMany(TeamUser::class, 'team_id');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'team_id');
    }

    public function pixels(): HasMany
    {
        return $this->hasMany(Pixel::class, 'team_id');
    }

    public function boards(): HasMany
    {
        return $this->hasMany(Board::class, 'team_id');
    }
}