<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class FolderWebhook extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;

    protected $table = 'FolderWebhook';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'folderId',
        'webhookId',
    ];

    public function folder(): BelongsTo
    {
        return $this->belongsTo(Folder::class, 'folderId');
    }

    public function webhook(): BelongsTo
    {
        return $this->belongsTo(Webhook::class, 'webhookId');
    }
}