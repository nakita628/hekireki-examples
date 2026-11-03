defmodule Teable.View do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          type: String.t(),
          sort: String.t() | nil,
          filter: String.t() | nil,
          group: String.t() | nil,
          options: String.t() | nil,
          order: float(),
          version: integer(),
          column_meta: String.t(),
          is_locked: boolean() | nil,
          enable_share: boolean() | nil,
          share_id: String.t() | nil,
          share_meta: String.t() | nil,
          created_time: DateTime.t(),
          deleted_time: DateTime.t() | nil,
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          table: Teable.TableMeta.t() | nil
        }

  schema "view" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:type, :string)
    field(:sort, :string)
    field(:filter, :string)
    field(:group, :string)
    field(:options, :string)
    field(:order, :float)
    field(:version, :integer)
    field(:column_meta, :string)
    field(:is_locked, :boolean)
    field(:enable_share, :boolean)
    field(:share_id, :string)
    field(:share_meta, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:deleted_time, Teable.PrismaDateTime)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    belongs_to(:table, Teable.TableMeta, foreign_key: :table_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end