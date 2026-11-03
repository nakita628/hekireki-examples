defmodule Trigger.AuthorizationCode do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          code: String.t(),
          requester_ip: String.t() | nil,
          code_challenge: String.t() | nil,
          callback_port: integer() | nil,
          approval_code_hash: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          personal_access_token: Trigger.PersonalAccessToken.t() | nil
        }

  schema "AuthorizationCode" do
    field(:id, :string, primary_key: true)
    field(:code, :string)
    field(:requester_ip, :string, source: :requesterIp)
    field(:code_challenge, :string, source: :codeChallenge)
    field(:callback_port, :integer, source: :callbackPort)
    field(:approval_code_hash, :string, source: :approvalCodeHash)
    field(:personal_access_token_id, :string, source: :personalAccessTokenId)
    belongs_to(:personal_access_token, Trigger.PersonalAccessToken, foreign_key: :personal_access_token_id, define_field: false, type: :string)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end