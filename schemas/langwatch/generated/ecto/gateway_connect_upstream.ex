defmodule LangWatch.GatewayConnectUpstream do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          organization_id: String.t(),
          base_url: String.t(),
          encrypted_token: String.t(),
          instance_id: String.t(),
          updated_at: DateTime.t()
        }

  schema "GatewayConnectUpstream" do
    field(:organization_id, :string, primary_key: true, source: :organizationId)
    field(:base_url, :string, source: :baseUrl)
    field(:encrypted_token, :string, source: :encryptedToken)
    field(:instance_id, :string, source: :instanceId)
    timestamps(inserted_at: false, updated_at_source: :updatedAt)
  end
end