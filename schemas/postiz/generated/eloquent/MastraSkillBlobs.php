<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class MastraSkillBlobs extends Model
{
    use PrismaDates;

    const CREATED_AT = 'createdAt';
    const UPDATED_AT = null;
    const ZONED_DATES = ['createdAtZ'];

    protected $table = 'mastra_skill_blobs';

    protected $primaryKey = 'hash';

    protected $keyType = 'string';

    public $incrementing = false;

    protected $fillable = [
        'hash',
        'content',
        'size',
        'mimeType',
        'createdAtZ',
    ];

    protected $casts = [
        'size' => 'integer',
        'createdAtZ' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('createdAtZ', $this->attributes)) {
                $this->setAttribute('createdAtZ', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}