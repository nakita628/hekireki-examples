defmodule Teable.TableMeta do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          icon: String.t() | nil,
          db_table_name: String.t(),
          db_view_name: String.t() | nil,
          search_index: map() | nil,
          search_index_policy: map() | nil,
          query_index_policy: map() | nil,
          provision_state: atom(),
          version: integer(),
          order: float(),
          created_time: DateTime.t(),
          deleted_time: DateTime.t() | nil,
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          base: Teable.Base.t() | nil,
          fields: [Teable.Field.t()],
          views: [Teable.View.t()],
          plugin_panel: [Teable.PluginPanel.t()],
          plugin_context_menu: [Teable.PluginContextMenu.t()]
        }

  schema "table_meta" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:icon, :string)
    field(:db_table_name, :string)
    field(:db_view_name, :string)
    field(:search_index, :map)
    field(:search_index_policy, :map)
    field(:query_index_policy, :map)
    field(:provision_state, Ecto.Enum, values: [:pending, :ready, :error, :deleting], default: :ready)
    field(:version, :integer)
    field(:order, :float)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:deleted_time, Teable.PrismaDateTime)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    belongs_to(:base, Teable.Base, foreign_key: :base_id, type: :string)
    has_many(:fields, Teable.Field, foreign_key: :table_id)
    has_many(:views, Teable.View, foreign_key: :table_id)
    has_many(:plugin_panel, Teable.PluginPanel, foreign_key: :table_id)
    has_many(:plugin_context_menu, Teable.PluginContextMenu, foreign_key: :table_id)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end