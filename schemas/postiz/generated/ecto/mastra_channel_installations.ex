defmodule Postiz.MastraChannelInstallations do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          platform: String.t(),
          agent_id: String.t(),
          status: String.t(),
          webhook_id: String.t() | nil,
          data: map(),
          config_hash: String.t() | nil,
          error: String.t() | nil,
          created_at: DateTime.t(),
          updated_at: DateTime.t(),
          created_at_z: DateTime.t() | nil,
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_channel_installations" do
    field(:id, :string, primary_key: true)
    field(:platform, :string)
    field(:agent_id, :string, source: :agentId)
    field(:status, :string)
    field(:webhook_id, :string, source: :webhookId)
    field(:data, :map)
    field(:config_hash, :string, source: :configHash)
    field(:error, :string)
    field(:created_at, Postiz.PrismaDateTime, source: :createdAt)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:created_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :createdAtZ)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end