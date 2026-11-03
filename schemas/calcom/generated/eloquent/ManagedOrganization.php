<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ManagedOrganization extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'ManagedOrganization';

    protected $primaryKey = 'managedOrganizationId';

    public $incrementing = false;

    protected $fillable = [
        'managedOrganizationId',
        'managerOrganizationId',
    ];

    protected $casts = [
        'managerOrganizationId' => 'integer',
    ];

    public function managedOrganization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'managedOrganizationId');
    }

    public function managerOrganization(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'managerOrganizationId');
    }
}