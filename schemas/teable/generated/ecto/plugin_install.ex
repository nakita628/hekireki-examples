defmodule Teable.PluginInstall do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          base_id: String.t(),
          name: String.t(),
          position_id: String.t(),
          position: String.t(),
          storage: String.t() | nil,
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          plugin: Teable.Plugin.t() | nil
        }

  schema "plugin_install" do
    field(:id, :string, primary_key: true)
    field(:base_id, :string)
    field(:name, :string)
    field(:position_id, :string)
    field(:position, :string)
    field(:storage, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    belongs_to(:plugin, Teable.Plugin, foreign_key: :plugin_id, type: :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end