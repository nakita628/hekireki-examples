<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;
use Illuminate\Database\Eloquent\Relations\HasMany;
use Illuminate\Database\Eloquent\Relations\HasOne;

class Dataroom extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = 'updatedAt';

    protected $table = 'Dataroom';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'pId',
        'name',
        'internalName',
        'description',
        'teamId',
        'brandId',
        'conversationsEnabled',
        'agentsEnabled',
        'vectorStoreId',
        'requestListEnabled',
        'enableChangeNotifications',
        'enableVisitorUploadChangeNotifications',
        'defaultPermissionStrategy',
        'defaultShowBanner',
        'defaultGroupPermissionStrategy',
        'defaultRootItemAccess',
        'defaultGroupRootItemAccess',
        'allowBulkDownload',
        'showLastUpdated',
        'introductionEnabled',
        'introductionContent',
        'isFrozen',
        'frozenAt',
        'frozenBy',
        'freezeArchiveUrl',
        'freezeArchiveHash',
    ];

    protected $attributes = [
        'conversationsEnabled' => false,
        'agentsEnabled' => false,
        'requestListEnabled' => false,
        'enableChangeNotifications' => false,
        'enableVisitorUploadChangeNotifications' => false,
        'defaultPermissionStrategy' => 'INHERIT_FROM_PARENT',
        'defaultShowBanner' => true,
        'defaultGroupPermissionStrategy' => 'INHERIT_FROM_PARENT',
        'defaultRootItemAccess' => 'VIEW_ONLY',
        'defaultGroupRootItemAccess' => 'VIEW_ONLY',
        'allowBulkDownload' => true,
        'showLastUpdated' => true,
        'introductionEnabled' => false,
        'isFrozen' => false,
    ];

    protected $casts = [
        'conversationsEnabled' => 'boolean',
        'agentsEnabled' => 'boolean',
        'requestListEnabled' => 'boolean',
        'enableChangeNotifications' => 'boolean',
        'enableVisitorUploadChangeNotifications' => 'boolean',
        'defaultPermissionStrategy' => DefaultPermissionStrategy::class,
        'defaultShowBanner' => 'boolean',
        'defaultGroupPermissionStrategy' => DefaultPermissionStrategy::class,
        'defaultRootItemAccess' => RootItemAccess::class,
        'defaultGroupRootItemAccess' => RootItemAccess::class,
        'allowBulkDownload' => 'boolean',
        'showLastUpdated' => 'boolean',
        'introductionEnabled' => 'boolean',
        'introductionContent' => 'array',
        'isFrozen' => 'boolean',
        'frozenAt' => 'datetime',
    ];

    public function team(): BelongsTo
    {
        return $this->belongsTo(Team::class, 'teamId');
    }

    public function baseBrand(): BelongsTo
    {
        return $this->belongsTo(Brand::class, 'brandId');
    }

    public function frozenByUser(): BelongsTo
    {
        return $this->belongsTo(User::class, 'frozenBy');
    }

    public function brand(): HasOne
    {
        return $this->hasOne(DataroomBrand::class, 'dataroomId');
    }

    public function documents(): HasMany
    {
        return $this->hasMany(DataroomDocument::class, 'dataroomId');
    }

    public function folders(): HasMany
    {
        return $this->hasMany(DataroomFolder::class, 'dataroomId');
    }

    public function links(): HasMany
    {
        return $this->hasMany(Link::class, 'dataroomId');
    }

    public function views(): HasMany
    {
        return $this->hasMany(View::class, 'dataroomId');
    }

    public function viewers(): HasMany
    {
        return $this->hasMany(Viewer::class, 'dataroomId');
    }

    public function viewerGroups(): HasMany
    {
        return $this->hasMany(ViewerGroup::class, 'dataroomId');
    }

    public function userDatarooms(): HasMany
    {
        return $this->hasMany(UserDataroom::class, 'dataroomId');
    }

    public function permissionGroups(): HasMany
    {
        return $this->hasMany(PermissionGroup::class, 'dataroomId');
    }

    public function conversations(): HasMany
    {
        return $this->hasMany(Conversation::class, 'dataroomId');
    }

    public function questions(): HasMany
    {
        return $this->hasMany(DataroomQuestion::class, 'dataroomId');
    }

    public function faqItems(): HasMany
    {
        return $this->hasMany(DataroomFaqItem::class, 'dataroomId');
    }

    public function chats(): HasMany
    {
        return $this->hasMany(Chat::class, 'dataroomId');
    }

    public function uploadedDocuments(): HasMany
    {
        return $this->hasMany(DocumentUpload::class, 'dataroomId');
    }

    public function tags(): HasMany
    {
        return $this->hasMany(TagItem::class, 'dataroomId');
    }

    public function taskLists(): HasMany
    {
        return $this->hasMany(TaskList::class, 'dataroomId');
    }

    public function tasks(): HasMany
    {
        return $this->hasMany(Task::class, 'dataroomId');
    }
}