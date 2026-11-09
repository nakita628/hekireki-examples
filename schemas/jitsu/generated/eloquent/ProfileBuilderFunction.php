<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ProfileBuilderFunction extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ProfileBuilderFunction';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'profileBuilderId',
        'functionId',
    ];

    public function profileBuilder(): BelongsTo
    {
        return $this->belongsTo(ProfileBuilder::class, 'profileBuilderId');
    }

    public function function(): BelongsTo
    {
        return $this->belongsTo(ConfigurationObject::class, 'functionId');
    }
}