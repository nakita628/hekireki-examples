<?php

namespace App\Models;

use Illuminate\Contracts\Database\Eloquent\CastsAttributes;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Support\Facades\Date;

class AsPrismaTime implements CastsAttributes
{
    public function get(Model $model, string $key, mixed $value, array $attributes): mixed
    {
        return $value === null ? null : Date::parse('1970-01-01 ' . preg_replace('/[+-]\d\d(:?\d\d)?$/', '', (string) $value), 'UTC');
    }

    public function set(Model $model, string $key, mixed $value, array $attributes): ?string
    {
        if (is_string($value) && preg_match('/^\d{2}:\d{2}(:\d{2}(\.\d+)?)?$/', $value)) {
            return $value . '+00:00';
        }

        return $value === null ? null : Date::parse($value)->setTimezone('UTC')->format('H:i:s.vP');
    }
}