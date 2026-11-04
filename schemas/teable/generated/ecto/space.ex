defmodule Teable.Space do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          avatar: String.t() | nil,
          credit: integer() | nil,
          deleted_time: DateTime.t() | nil,
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          is_template: boolean() | nil,
          updated_at: DateTime.t() | nil,
          data_db_binding: Teable.SpaceDataDbBinding.t() | nil,
          base_group: [Teable.Base.t()],
          data_db_migration_jobs: [Teable.SpaceDataDbMigrationJob.t()],
          base_data_db_move_jobs_as_source: [Teable.BaseDataDbMoveJob.t()],
          base_data_db_move_jobs_as_target: [Teable.BaseDataDbMoveJob.t()]
        }

  schema "space" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:avatar, :string)
    field(:credit, :integer)
    field(:deleted_time, Teable.PrismaDateTime)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    field(:is_template, :boolean)
    has_one(:data_db_binding, Teable.SpaceDataDbBinding, foreign_key: :space_id, references: :id)
    has_many(:base_group, Teable.Base, foreign_key: :space_id, references: :id)
    has_many(:data_db_migration_jobs, Teable.SpaceDataDbMigrationJob, foreign_key: :space_id, references: :id)
    has_many(:base_data_db_move_jobs_as_source, Teable.BaseDataDbMoveJob, foreign_key: :source_space_id, references: :id)
    has_many(:base_data_db_move_jobs_as_target, Teable.BaseDataDbMoveJob, foreign_key: :target_space_id, references: :id)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end