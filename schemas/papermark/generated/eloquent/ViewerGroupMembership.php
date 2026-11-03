<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class ViewerGroupMembership extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'ViewerGroupMembership';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'viewerId',
        'groupId',
    ];

    public function viewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'viewerId');
    }

    public function group(): BelongsTo
    {
        return $this->belongsTo(ViewerGroup::class, 'groupId');
    }
}