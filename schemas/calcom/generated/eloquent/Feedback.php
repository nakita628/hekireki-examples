<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Feedback extends Model
{
    use PrismaDates;

    protected $table = 'Feedback';

    public $timestamps = false;

    protected $fillable = [
        'date',
        'userId',
        'rating',
        'comment',
    ];

    protected $casts = [
        'date' => 'datetime',
        'userId' => 'integer',
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

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'userId');
    }
}