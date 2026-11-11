<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\Relations\HasOne;

class LiteLLMModelTable extends Model
{
    use PrismaDates;

    protected $table = 'LiteLLM_ModelTable';

    protected $fillable = [
        'aliases',
        'created_by',
        'updated_by',
    ];

    protected $casts = [
        'aliases' => 'array',
    ];

    public function team(): HasOne
    {
        return $this->hasOne(LiteLLMTeamTable::class, 'model_id');
    }
}