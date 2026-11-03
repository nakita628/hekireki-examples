defmodule Teable.Notification do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          id: String.t(),
          from_user_id: String.t(),
          to_user_id: String.t(),
          type: String.t(),
          message: String.t(),
          message_i18n: String.t() | nil,
          severity: String.t(),
          url_path: String.t() | nil,
          is_read: boolean(),
          created_time: DateTime.t(),
          created_by: String.t()
        }

  schema "notification" do
    field(:id, :string, primary_key: true)
    field(:from_user_id, :string)
    field(:to_user_id, :string)
    field(:type, :string)
    field(:message, :string)
    field(:message_i18n, :string)
    field(:severity, :string, default: "info")
    field(:url_path, :string)
    field(:is_read, :boolean, default: false)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
  end
end