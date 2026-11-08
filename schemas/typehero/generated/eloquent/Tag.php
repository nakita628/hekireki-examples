<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class Tag extends Model
{
    use HasVersion4Uuids;

    protected $table = 'Tag';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'tag',
    ];

    protected $casts = [
        'tag' => Tags::class,
    ];

    public function challenges(): BelongsToMany
    {
        return $this->belongsToMany(Challenge::class, '_ChallengeToTag', 'B', 'A');
    }
}