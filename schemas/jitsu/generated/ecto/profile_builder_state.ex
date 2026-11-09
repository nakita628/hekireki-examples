defmodule Jitsu.ProfileBuilderState do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          profile_builder_id: String.t(),
          profile_builder_version: integer(),
          instance_index: integer(),
          total_instances: integer(),
          started_at: DateTime.t(),
          updated_at: DateTime.t(),
          last_timestamp: DateTime.t() | nil,
          processed_users: integer(),
          total_users: integer(),
          error_users: integer(),
          speed: float()
        }

  schema "ProfileBuilderState" do
    field(:profile_builder_id, :string, primary_key: true, source: :profileBuilderId)
    field(:profile_builder_version, :integer, primary_key: true, source: :profileBuilderVersion)
    field(:instance_index, :integer, primary_key: true, source: :instanceIndex)
    field(:total_instances, :integer, primary_key: true, source: :totalInstances)
    field(:started_at, Jitsu.PrismaDateTime, source: :startedAt)
    field(:updated_at, Jitsu.PrismaDateTime, source: :updatedAt)
    field(:last_timestamp, Jitsu.PrismaDateTime, source: :lastTimestamp)
    field(:processed_users, :integer, source: :processedUsers)
    field(:total_users, :integer, source: :totalUsers)
    field(:error_users, :integer, source: :errorUsers)
    field(:speed, :float)
  end
end