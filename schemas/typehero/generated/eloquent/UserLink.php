<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsToMany;

class UserLink extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'UserLink';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'url',
    ];

    public function user(): BelongsToMany
    {
        return $this->belongsToMany(User::class, '_UserToUserLink', 'B', 'A');
    }
}