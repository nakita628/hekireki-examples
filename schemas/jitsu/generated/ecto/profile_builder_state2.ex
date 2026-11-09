defmodule Jitsu.ProfileBuilderState2 do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          profile_builder_id: String.t(),
          updated_at: DateTime.t(),
          full_rebuild_info: map() | nil,
          queues_info: map() | nil,
          metrics: map() | nil
        }

  schema "ProfileBuilderState2" do
    field(:profile_builder_id, :string, primary_key: true, source: :profileBuilderId)
    field(:updated_at, Jitsu.PrismaDateTime, source: :updatedAt)
    field(:full_rebuild_info, :map, source: :fullRebuildInfo)
    field(:queues_info, :map, source: :queuesInfo)
    field(:metrics, :map)
  end
end