defmodule Teable.Plugin do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          name: String.t(),
          description: String.t() | nil,
          detail_desc: String.t() | nil,
          logo: String.t(),
          help_url: String.t() | nil,
          status: String.t(),
          positions: String.t(),
          url: String.t() | nil,
          secret: String.t(),
          masked_secret: String.t(),
          i18n: String.t() | nil,
          config: String.t() | nil,
          plugin_user: String.t() | nil,
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil,
          plugin_install: [Teable.PluginInstall.t()]
        }

  schema "plugin" do
    field(:id, :string, primary_key: true)
    field(:name, :string)
    field(:description, :string)
    field(:detail_desc, :string)
    field(:logo, :string)
    field(:help_url, :string)
    field(:status, :string)
    field(:positions, :string)
    field(:url, :string)
    field(:secret, :string)
    field(:masked_secret, :string)
    field(:i18n, :string)
    field(:config, :string)
    field(:plugin_user, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    has_many(:plugin_install, Teable.PluginInstall, foreign_key: :plugin_id)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end