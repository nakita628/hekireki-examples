defmodule LangWatch.GatewayRealtimeSessionReport do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: LangWatch.PrismaDateTime, autogenerate: {LangWatch.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          session_id: String.t(),
          report_key: String.t(),
          project_id: String.t(),
          model: String.t(),
          usage: map(),
          cost_nano_usd: integer(),
          inserted_at: DateTime.t()
        }

  schema "GatewayRealtimeSessionReport" do
    field(:id, :string, primary_key: true)
    field(:session_id, :string, source: :sessionId)
    field(:report_key, :string, source: :reportKey)
    field(:project_id, :string, source: :projectId)
    field(:model, :string)
    field(:usage, :map)
    field(:cost_nano_usd, :integer, source: :costNanoUsd)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end