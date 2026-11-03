<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Relations\BelongsTo;

class Survey extends \Illuminate\Database\Eloquent\Model
{
    use PrismaDates;

    const UPDATED_AT = null;

    protected $table = 'surveys';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'id',
        'survey_name',
        'response',
        'user_id',
        'user_email',
        'org_id',
    ];

    protected $casts = [
        'survey_name' => SurveyName::class,
        'response' => 'array',
    ];

    public function org(): BelongsTo
    {
        return $this->belongsTo(Organization::class, 'org_id');
    }

    public function user(): BelongsTo
    {
        return $this->belongsTo(User::class, 'user_id');
    }
}