defmodule Postiz.MastraChannelConfig do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          platform: String.t(),
          data: map(),
          updated_at: DateTime.t(),
          updated_at_z: DateTime.t() | nil
        }

  schema "mastra_channel_config" do
    field(:platform, :string, primary_key: true)
    field(:data, :map)
    field(:updated_at, Postiz.PrismaDateTime, source: :updatedAt)
    field(:updated_at_z, Postiz.PrismaDateTime, autogenerate: true, source: :updatedAtZ)
  end
end