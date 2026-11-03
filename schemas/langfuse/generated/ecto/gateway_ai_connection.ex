defmodule Langfuse.GatewayAiConnection do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Langfuse.PrismaDateTime, autogenerate: {Langfuse.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          provider: atom(),
          encrypted_credential: String.t(),
          display_secret: String.t(),
          created_by_id: String.t() | nil,
          status: atom(),
          allowed_models: map() | nil,
          inserted_at: DateTime.t(),
          updated_at: DateTime.t(),
          organization: Langfuse.Organization.t() | nil
        }

  schema "gateway_ai_connections" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:provider, Ecto.Enum, values: [OPENAI: "openai", ANTHROPIC: "anthropic"])
    field(:encrypted_credential, :string, source: :encrypted_credentials)
    field(:display_secret, :string, source: :display_secret_key)
    field(:created_by_id, :string)
    field(:status, Ecto.Enum, values: [ENABLED: "enabled", DISABLED: "disabled", ERROR: "error"], default: :ENABLED)
    field(:allowed_models, :map)
    belongs_to(:organization, Langfuse.Organization, foreign_key: :organization_id, type: :string)
    timestamps(inserted_at_source: :created_at)
  end
end