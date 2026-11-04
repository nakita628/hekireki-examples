defmodule Teable.Base do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t() | nil,
          order: float(),
          icon: String.t() | nil,
          schema_pass: String.t() | nil,
          v2_enabled: boolean(),
          provision_state: atom(),
          deleted_time: DateTime.t() | nil,
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          space: Teable.Space.t() | nil,
          tables: [Teable.TableMeta.t()],
          data_db_move_jobs: [Teable.BaseDataDbMoveJob.t()],
          user_orders: [Teable.UserBaseOrder.t()]
        }

  schema "base" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:order, :float)
    field(:icon, :string)
    field(:schema_pass, :string)
    field(:v2_enabled, :boolean, default: false)
    field(:provision_state, Ecto.Enum, values: [:pending, :ready, :error, :deleting], default: :ready)
    field(:deleted_time, Teable.PrismaDateTime)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    belongs_to(:space, Teable.Space, foreign_key: :space_id, type: :string)
    has_many(:tables, Teable.TableMeta, foreign_key: :base_id)
    has_many(:data_db_move_jobs, Teable.BaseDataDbMoveJob, foreign_key: :base_id)
    has_many(:user_orders, Teable.UserBaseOrder, foreign_key: :base_id)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end