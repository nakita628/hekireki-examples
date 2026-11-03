defmodule Rallly.InstanceSettings do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Rallly.PrismaDateTime, autogenerate: {Rallly.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          disable_user_registration: boolean(),
          instance_id: String.t(),
          app_name: String.t() | nil,
          primary_color: String.t() | nil,
          primary_color_dark: String.t() | nil,
          logo: String.t() | nil,
          logo_dark: String.t() | nil,
          logo_icon: String.t() | nil,
          hide_attribution: boolean() | nil,
          footer_links: map(),
          inserted_at: DateTime.t(),
          updated_at: DateTime.t()
        }

  schema "instance_settings" do
    field(:id, :integer, primary_key: true, default: 1)
    field(:disable_user_registration, :boolean, default: false)
    field(:instance_id, :string, read_after_writes: true)
    field(:app_name, :string)
    field(:primary_color, :string)
    field(:primary_color_dark, :string)
    field(:logo, :string)
    field(:logo_dark, :string)
    field(:logo_icon, :string)
    field(:hide_attribution, :boolean)
    field(:footer_links, :map)
    timestamps(inserted_at_source: :created_at)
  end
end