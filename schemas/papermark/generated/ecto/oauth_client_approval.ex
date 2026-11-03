defmodule Papermark.OAuthClientApproval do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          user_id: String.t(),
          client_id: String.t(),
          approved_at: DateTime.t(),
          revoked_at: DateTime.t() | nil,
          client_metadata_hash: String.t() | nil
        }

  schema "OAuthClientApproval" do
    field(:id, :string, primary_key: true)
    field(:user_id, :string, source: :userId)
    field(:client_id, :string, source: :clientId)
    field(:approved_at, Papermark.PrismaDateTime, autogenerate: true, source: :approvedAt)
    field(:revoked_at, Papermark.PrismaDateTime, source: :revokedAt)
    field(:client_metadata_hash, :string, source: :clientMetadataHash)
  end
end