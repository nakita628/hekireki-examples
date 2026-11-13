defmodule SuperProductivity.User do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: SuperProductivity.PrismaDateTime, autogenerate: {SuperProductivity.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          email: String.t(),
          password_hash: String.t() | nil,
          is_verified: integer(),
          verification_token: String.t() | nil,
          verification_token_expires_at: integer() | nil,
          verification_resend_count: integer(),
          reset_password_token: String.t() | nil,
          reset_password_token_expires_at: integer() | nil,
          passkey_recovery_token: String.t() | nil,
          passkey_recovery_token_expires_at: integer() | nil,
          login_token: String.t() | nil,
          login_token_expires_at: integer() | nil,
          failed_login_attempts: integer(),
          locked_until: integer() | nil,
          token_version: integer(),
          terms_accepted_at: integer() | nil,
          storage_quota_bytes: integer(),
          storage_used_bytes: integer(),
          inserted_at: DateTime.t(),
          sync_state: SuperProductivity.UserSyncState.t() | nil,
          capture_key: SuperProductivity.CaptureKey.t() | nil,
          capture_connect_code: SuperProductivity.CaptureConnectCode.t() | nil,
          operations: [SuperProductivity.Operation.t()],
          devices: [SuperProductivity.SyncDevice.t()],
          passkeys: [SuperProductivity.Passkey.t()],
          pending_passkey_registrations: [SuperProductivity.PendingPasskeyRegistration.t()],
          capture_grants: [SuperProductivity.CaptureGrant.t()],
          capture_items: [SuperProductivity.CaptureItem.t()],
          capture_authorizations: [SuperProductivity.CaptureAuth.t()],
          connect_requests: [SuperProductivity.ConnectRequest.t()]
        }

  schema "users" do
    field(:email, :string)
    field(:password_hash, :string)
    field(:is_verified, :integer, default: 0)
    field(:verification_token, :string)
    field(:verification_token_expires_at, :integer)
    field(:verification_resend_count, :integer, default: 0)
    field(:reset_password_token, :string)
    field(:reset_password_token_expires_at, :integer)
    field(:passkey_recovery_token, :string)
    field(:passkey_recovery_token_expires_at, :integer)
    field(:login_token, :string)
    field(:login_token_expires_at, :integer)
    field(:failed_login_attempts, :integer, default: 0)
    field(:locked_until, :integer)
    field(:token_version, :integer, default: 0)
    field(:terms_accepted_at, :integer)
    field(:storage_quota_bytes, :integer, default: 104857600)
    field(:storage_used_bytes, :integer, default: 0)
    has_one(:sync_state, SuperProductivity.UserSyncState, foreign_key: :user_id)
    has_one(:capture_key, SuperProductivity.CaptureKey, foreign_key: :user_id)
    has_one(:capture_connect_code, SuperProductivity.CaptureConnectCode, foreign_key: :user_id)
    has_many(:operations, SuperProductivity.Operation, foreign_key: :user_id)
    has_many(:devices, SuperProductivity.SyncDevice, foreign_key: :user_id)
    has_many(:passkeys, SuperProductivity.Passkey, foreign_key: :user_id)
    has_many(:pending_passkey_registrations, SuperProductivity.PendingPasskeyRegistration, foreign_key: :user_id)
    has_many(:capture_grants, SuperProductivity.CaptureGrant, foreign_key: :user_id)
    has_many(:capture_items, SuperProductivity.CaptureItem, foreign_key: :user_id)
    has_many(:capture_authorizations, SuperProductivity.CaptureAuth, foreign_key: :user_id)
    has_many(:connect_requests, SuperProductivity.ConnectRequest, foreign_key: :user_id)
    timestamps(inserted_at_source: :created_at, updated_at: false)
  end
end