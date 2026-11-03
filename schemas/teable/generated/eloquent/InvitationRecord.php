<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class InvitationRecord extends Model
{
    use PrismaDates;

    protected $table = 'invitation_record';

    protected $keyType = 'string';

    public $incrementing = false;

    public $timestamps = false;

    protected $fillable = [
        'id',
        'invitation_id',
        'base_id',
        'space_id',
        'type',
        'inviter',
        'accepter',
        'created_time',
    ];

    protected $casts = [
        'created_time' => 'datetime',
    ];

    public function save(array $options = [])
    {
        if (! $this->exists) {
            if (! array_key_exists('created_time', $this->attributes)) {
                $this->setAttribute('created_time', $this->freshTimestamp());
            }
        }

        return parent::save($options);
    }
}