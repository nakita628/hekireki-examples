defmodule Teable.AttachmentsTable do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          attachment_id: String.t(),
          name: String.t(),
          token: String.t(),
          table_id: String.t(),
          record_id: String.t(),
          field_id: String.t(),
          created_time: DateTime.t(),
          created_by: String.t(),
          last_modified_by: String.t() | nil,
          updated_at: DateTime.t() | nil
        }

  schema "attachments_table" do
    field(:id, :string, primary_key: true)
    field(:attachment_id, :string)
    field(:name, :string)
    field(:token, :string)
    field(:table_id, :string)
    field(:record_id, :string)
    field(:field_id, :string)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:created_by, :string)
    field(:last_modified_by, :string)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end