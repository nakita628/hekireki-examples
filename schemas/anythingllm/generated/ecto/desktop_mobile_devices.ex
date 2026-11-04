defmodule AnythingLLM.DesktopMobileDevices do
  use Ecto.Schema

  @primary_key {:id, :id, autogenerate: true}
  @timestamps_opts [type: AnythingLLM.PrismaDateTime, autogenerate: {AnythingLLM.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: integer(),
          device_os: String.t(),
          device_name: String.t(),
          token: String.t(),
          approved: boolean(),
          inserted_at: DateTime.t(),
          user: AnythingLLM.Users.t() | nil
        }

  schema "desktop_mobile_devices" do
    field(:device_os, :string, source: :deviceOs)
    field(:device_name, :string, source: :deviceName)
    field(:token, :string)
    field(:approved, :boolean, default: false)
    field(:user_id, :id, source: :userId)
    belongs_to(:user, AnythingLLM.Users, foreign_key: :user_id, define_field: false)
    timestamps(inserted_at_source: :createdAt, updated_at: false)
  end
end