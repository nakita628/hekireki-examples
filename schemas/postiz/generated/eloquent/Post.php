<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;

class Post extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Post';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'state',
        'publishDate',
        'organizationId',
        'integrationId',
        'content',
        'delay',
        'group',
        'batchId',
        'title',
        'description',
        'parentPostId',
        'releaseId',
        'releaseURL',
        'settings',
        'image',
        'submittedForOrderId',
        'submittedForOrganizationId',
        'approvedSubmitForOrder',
        'creationMethod',
        'lastMessageId',
        'intervalInDays',
        'error',
        'deletedAt',
    ];

    protected $attributes = [
        'state' => 'QUEUE',
        'delay' => 0,
        'approvedSubmitForOrder' => 'NO',
        'creationMethod' => 'UNKNOWN',
    ];

    protected $casts = [
        'state' => State::class,
        'publishDate' => 'datetime',
        'delay' => 'integer',
        'approvedSubmitForOrder' => APPROVED_SUBMIT_FOR_ORDER::class,
        'creationMethod' => CreationMethod::class,
        'intervalInDays' => 'integer',
        'deletedAt' => 'datetime',
    ];

    public function integration(): BelongsTo
    {
        return $this->belongsTo(Integration::class, 'integrationId');
    }

    public function lastMessage(): BelongsTo
    {
        return $this->belongsTo(Messages::class, 'lastMessageId');
    }

    public function organization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'organizationId');
    }

    public function parentPost(): BelongsTo
    {
        return $this->belongsTo(Post::class, 'parentPostId');
    }

    public function submittedForOrder(): BelongsTo
    {
        return $this->belongsTo(Orders::class, 'submittedForOrderId');
    }

    public function submittedForOrganization(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'submittedForOrganizationId');
    }

    public function comments(): HasMany
    {
        return $this->hasMany(Comments::class, 'postId');
    }

    public function errors(): HasMany
    {
        return $this->hasMany(Errors::class, 'postId');
    }

    public function payoutProblems(): HasMany
    {
        return $this->hasMany(PayoutProblems::class, 'postId');
    }

    public function childrenPost(): HasMany
    {
        return $this->hasMany(Post::class, 'parentPostId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(TagsPosts::class, 'postId');
    }
}