defmodule Jitsu.FunctionsServer do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Jitsu.PrismaDateTime, autogenerate: {Jitsu.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          workspace_id: String.t(),
          class: String.t(),
          deployment_id: String.t(),
          connections: [String.t()],
          empty_connections: [String.t()],
          profile_builders: [String.t()],
          updated_at: DateTime.t() | nil,
          inserted_at: DateTime.t()
        }

  schema "FunctionsServer" do
    field(:workspace_id, :string, primary_key: true, source: :workspaceId)
    field(:class, :string, primary_key: true)
    field(:deployment_id, :string, source: :deploymentId)
    field(:connections, {:array, :string})
    field(:empty_connections, {:array, :string}, source: :emptyConnections)
    field(:profile_builders, {:array, :string}, source: :profileBuilders)
    field(:updated_at, Jitsu.PrismaDateTime, source: :updatedAt)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end