<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Model;

class Settings extends Model
{
    protected $table = 'settings';

    public $timestamps = false;

    protected $fillable = [
        'rateLimitWindow',
        'rateLimitMax',
        'secret',
        'serviceName',
        'chunkSize',
        'chunkedUploadsTimeout',
        'maxSize',
        'generateZips',
        'generateOriginalFileNameWithIdentifier',
        'generatedFilenameLength',
        'generatedAlbumLength',
        'generatedLinksLength',
        'saveDuplicatesToAlbum',
        'blockedExtensions',
        'blockNoExtension',
        'publicMode',
        'userAccounts',
        'disableStatisticsCron',
        'disableUpdateCheck',
        'backgroundImageURL',
        'logoURL',
        'metaDescription',
        'metaKeywords',
        'metaTwitterHandle',
        'metaDomain',
        'serveUploadsFrom',
        'enableMixedCaseFilenames',
        'usersStorageQuota',
        'useNetworkStorage',
        'useMinimalHomepage',
        'useUrlShortener',
        'generateThumbnails',
        'privacyPolicyPageContent',
        'termsOfServicePageContent',
        'rulesPageContent',
        'S3Region',
        'S3Bucket',
        'S3AccessKey',
        'S3SecretKey',
        'S3Endpoint',
        'S3PathStyle',
        'S3PublicUrl',
        'defaultSortOrder',
    ];

    protected $attributes = [
        'generateOriginalFileNameWithIdentifier' => false,
        'generatedLinksLength' => 8,
        'saveDuplicatesToAlbum' => false,
        'disableUpdateCheck' => false,
        'metaDomain' => '',
        'serveUploadsFrom' => '',
        'enableMixedCaseFilenames' => true,
        'usersStorageQuota' => '0',
        'useNetworkStorage' => false,
        'useMinimalHomepage' => false,
        'useUrlShortener' => false,
        'generateThumbnails' => true,
        'privacyPolicyPageContent' => '',
        'termsOfServicePageContent' => '',
        'rulesPageContent' => '',
        'S3Region' => '',
        'S3Bucket' => '',
        'S3AccessKey' => '',
        'S3SecretKey' => '',
        'S3Endpoint' => '',
        'S3PathStyle' => false,
        'S3PublicUrl' => '',
        'defaultSortOrder' => 'createdAt:desc',
    ];

    protected $casts = [
        'rateLimitWindow' => 'integer',
        'rateLimitMax' => 'integer',
        'chunkedUploadsTimeout' => 'integer',
        'generateZips' => 'boolean',
        'generateOriginalFileNameWithIdentifier' => 'boolean',
        'generatedFilenameLength' => 'integer',
        'generatedAlbumLength' => 'integer',
        'generatedLinksLength' => 'integer',
        'saveDuplicatesToAlbum' => 'boolean',
        'blockNoExtension' => 'boolean',
        'publicMode' => 'boolean',
        'userAccounts' => 'boolean',
        'disableStatisticsCron' => 'boolean',
        'disableUpdateCheck' => 'boolean',
        'enableMixedCaseFilenames' => 'boolean',
        'useNetworkStorage' => 'boolean',
        'useMinimalHomepage' => 'boolean',
        'useUrlShortener' => 'boolean',
        'generateThumbnails' => 'boolean',
        'S3PathStyle' => 'boolean',
    ];
}