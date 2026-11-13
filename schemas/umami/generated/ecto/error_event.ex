defmodule Umami.ErrorEvent do
  use Ecto.Schema

  @primary_key false

  @type t :: %__MODULE__{
          website_id: String.t(),
          id: String.t(),
          issue_id: String.t(),
          session_id: String.t(),
          visit_id: String.t(),
          created_at: DateTime.t(),
          received_at: DateTime.t(),
          name: String.t(),
          message: String.t(),
          stack: String.t(),
          frames: map(),
          tags: map(),
          handled: boolean(),
          url_path: String.t(),
          release: String.t(),
          environment: String.t(),
          browser: String.t(),
          os: String.t(),
          device: String.t()
        }

  schema "error_event" do
    field(:website_id, :string, primary_key: true)
    field(:id, :string, primary_key: true, source: :event_id)
    field(:issue_id, :string)
    field(:session_id, :string)
    field(:visit_id, :string)
    field(:created_at, Umami.PrismaDateTime)
    field(:received_at, Umami.PrismaDateTime, autogenerate: true)
    field(:name, :string)
    field(:message, :string)
    field(:stack, :string)
    field(:frames, :map)
    field(:tags, :map)
    field(:handled, :boolean)
    field(:url_path, :string)
    field(:release, :string)
    field(:environment, :string)
    field(:browser, :string)
    field(:os, :string)
    field(:device, :string)
  end
end