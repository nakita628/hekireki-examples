defmodule LangWatch.GatewayTraceExportKey do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          project_id: String.t(),
          api_key_id: String.t(),
          encrypted_token: String.t(),
          inserted_at: DateTime.t()
        }

  schema "GatewayTraceExportKey" do
    field(:project_id, :string, primary_key: true, source: :projectId)
    field(:api_key_id, :string, source: :apiKeyId)
    field(:encrypted_token, :string, source: :encryptedToken)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end