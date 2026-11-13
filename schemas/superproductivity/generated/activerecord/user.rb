class User < ApplicationRecord
  attribute :is_verified, default: 0
  attribute :verification_resend_count, default: 0
  attribute :failed_login_attempts, default: 0
  attribute :token_version, default: 0
  attribute :storage_quota_bytes, default: 104_857_600
  attribute :storage_used_bytes, default: 0

  validates :email, presence: true, uniqueness: true

  has_one :sync_state, class_name: "UserSyncState", inverse_of: :user, dependent: :destroy
  has_one :capture_key, dependent: :destroy
  has_one :capture_connect_code, dependent: :destroy
  has_many :operations, dependent: :destroy
  has_many :devices, class_name: "SyncDevice", inverse_of: :user, dependent: :destroy
  has_many :passkeys, dependent: :destroy
  has_many :pending_passkey_registrations, dependent: :destroy
  has_many :capture_grants, dependent: :destroy
  has_many :capture_items, dependent: :destroy
  has_many :capture_authorizations, class_name: "CaptureAuth", inverse_of: :user, dependent: :destroy
  has_many :connect_requests, dependent: :destroy
end
