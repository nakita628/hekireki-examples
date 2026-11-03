defmodule Teable.SpaceDataDbBinding do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          mode: atom(),
          state: atom(),
          created_by: String.t(),
          created_time: DateTime.t(),
          last_history_flushed_at: DateTime.t() | nil,
          last_removal_flushed_at: DateTime.t() | nil,
          updated_at: DateTime.t() | nil,
          space: Teable.Space.t() | nil,
          data_db_connection: Teable.DataDbConnection.t() | nil
        }

  schema "space_data_db_binding" do
    field(:id, :string, primary_key: true)
    field(:mode, Ecto.Enum, values: [:default, :byodb], default: :default)
    field(:state, Ecto.Enum, values: [:ready, :validating, :initializing, :migrating, :error, :disabled], default: :ready)
    field(:created_by, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:last_history_flushed_at, Teable.PrismaDateTime)
    field(:last_removal_flushed_at, Teable.PrismaDateTime)
    belongs_to(:space, Teable.Space, foreign_key: :space_id, type: :string)
    belongs_to(:data_db_connection, Teable.DataDbConnection, foreign_key: :data_db_connection_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end