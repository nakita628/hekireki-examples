<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class OidcLoginGroup extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'OidcLoginGroup';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'workspaceId',
        'oidcProviderId',
        'groupValue',
        'allowAllUsers',
    ];

    protected $attributes = [
        'allowAllUsers' => false,
    ];

    protected $casts = [
        'allowAllUsers' => 'boolean',
    ];

    public function workspace(): BelongsTo
    {
        return $this->belongsTo(Workspace::class, 'workspaceId');
    }

    public function oidcProvider(): BelongsTo
    {
        return $this->belongsTo(OidcProvider::class, 'oidcProviderId');
    }
}