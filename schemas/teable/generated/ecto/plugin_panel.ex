defmodule Teable.PluginPanel do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          layout: String.t() | nil,
          created_by: String.t(),
          created_time: DateTime.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          table: Teable.TableMeta.t() | nil
        }

  schema "plugin_panel" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:layout, :string)
    field(:created_by, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:last_modified_by, :string)
    belongs_to(:table, Teable.TableMeta, foreign_key: :table_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end