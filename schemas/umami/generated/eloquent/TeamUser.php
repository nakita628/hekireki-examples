<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class TeamUser extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at'];

    protected $table = 'team_user';

    protected $primaryKey = 'team_user_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'team_user_id',
        'team_id',
        'user_id',
        'role',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'team_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}