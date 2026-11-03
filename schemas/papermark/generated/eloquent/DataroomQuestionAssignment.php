<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class DataroomQuestionAssignment extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'DataroomQuestionAssignment';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'questionId',
        'linkId',
        'groupId',
        'viewerId',
        'email',
    ];

    public function question(): BelongsTo
    {
        return $this->belongsTo(DataroomQuestion::class, 'questionId');
    }

    public function link(): BelongsTo
    {
        return $this->belongsTo(Link::class, 'linkId');
    }

    public function group(): BelongsTo
    {
        return $this->belongsTo(ViewerGroup::class, 'groupId');
    }

    public function viewer(): BelongsTo
    {
        return $this->belongsTo(Viewer::class, 'viewerId');
    }
}