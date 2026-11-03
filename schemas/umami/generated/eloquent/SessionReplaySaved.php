<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class SessionReplaySaved extends Model
{
    use PrismaDates;

    const ZONED_DATES = ['created_at', 'updated_at'];

    protected $table = 'session_replay_saved';

    protected $primaryKey = 'saved_replay_id';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'saved_replay_id',
        'name',
        'website_id',
        'visit_id',
    ];

    public function website(): BelongsTo
    {
        return $this->belongsTo(Website::class, 'website_id');
    }
}