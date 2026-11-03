<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Concerns\HasVersion4Uuids;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\BelongsTo;

class NonprofitApplication extends Model
{
    use HasVersion4Uuids;
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'nonprofit_applications';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'space_id',
        'user_id',
        'organization_name',
        'website',
        'email_domain',
        'status',
        'reason',
        'model',
    ];

    protected $casts = [
        'status' => NonprofitApplicationStatus::class,
    ];

    public function space(): BelongsTo
    {
        return $this->belongsTo(Space::class, 'space_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}