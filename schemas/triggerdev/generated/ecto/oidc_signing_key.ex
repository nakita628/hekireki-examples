defmodule Trigger.OidcSigningKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Trigger.PrismaDateTime, autogenerate: {Trigger.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          kid: String.t(),
          purpose: atom(),
          status: atom(),
          public_jwk: map(),
          ready_at: DateTime.t(),
          public_until: DateTime.t() | nil,
          generated_by_user_id: String.t(),
          activated_at: DateTime.t() | nil,
          activated_by_user_id: String.t() | nil,
          retired_at: DateTime.t() | nil,
          retired_by_user_id: String.t() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "OidcSigningKey" do
    field(:id, :string, primary_key: true)
    field(:kid, :string)
    field(:purpose, Ecto.Enum, values: [:EXTERNAL_OIDC, :INTERNAL_RUN], default: :EXTERNAL_OIDC)
    field(:status, Ecto.Enum, values: [:STAGED, :ACTIVE, :RETIRED])
    field(:public_jwk, :map, source: :publicJwk)
    field(:ready_at, Trigger.PrismaDateTime, source: :readyAt)
    field(:public_until, Trigger.PrismaDateTime, source: :publicUntil)
    field(:generated_by_user_id, :string, source: :generatedByUserId)
    field(:activated_at, Trigger.PrismaDateTime, source: :activatedAt)
    field(:activated_by_user_id, :string, source: :activatedByUserId)
    field(:retired_at, Trigger.PrismaDateTime, source: :retiredAt)
    field(:retired_by_user_id, :string, source: :retiredByUserId)
    timestamps(inserted_at_source: :createdAt, updated_at_source: :updatedAt)
  end
end