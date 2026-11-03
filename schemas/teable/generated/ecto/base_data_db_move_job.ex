defmodule Teable.BaseDataDbMoveJob do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          source_connection_id: String.t() | nil,
          target_connection_id: String.t() | nil,
          state: atom(),
          inventory: map() | nil,
          copy_stats: map() | nil,
          validation_stats: map() | nil,
          last_error: String.t() | nil,
          started_at: DateTime.t() | nil,
          completed_at: DateTime.t() | nil,
          created_by: String.t(),
          created_time: DateTime.t(),
          updated_at: DateTime.t() | nil,
          base: Teable.Base.t() | nil,
          source_space: Teable.Space.t() | nil,
          target_space: Teable.Space.t() | nil
        }

  schema "base_data_db_move_job" do
    field(:id, :string, primary_key: true)
    field(:source_connection_id, :string)
    field(:target_connection_id, :string)
    field(:state, Ecto.Enum, values: [:pending, :waiting_worker, :copying_base_schema, :copying_shared_rows, :validating, :switching, :succeeded, :failed, :cancelled], default: :pending)
    field(:inventory, :map)
    field(:copy_stats, :map)
    field(:validation_stats, :map)
    field(:last_error, :string)
    field(:started_at, Teable.PrismaDateTime)
    field(:completed_at, Teable.PrismaDateTime)
    field(:created_by, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    belongs_to(:base, Teable.Base, foreign_key: :base_id, type: :string)
    belongs_to(:source_space, Teable.Space, foreign_key: :source_space_id, type: :string)
    belongs_to(:target_space, Teable.Space, foreign_key: :target_space_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end