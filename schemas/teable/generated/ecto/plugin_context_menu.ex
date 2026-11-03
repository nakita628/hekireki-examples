defmodule Teable.PluginContextMenu do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          plugin_install_id: String.t(),
          order: float(),
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          table: Teable.TableMeta.t() | nil
        }

  schema "plugin_context_menu" do
    field(:id, :string, primary_key: true)
    field(:plugin_install_id, :string)
    field(:order, :float)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    belongs_to(:table, Teable.TableMeta, foreign_key: :table_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end