defmodule Teable.Invitation do
  use Ecto.Schema

  @primary_key false
  @timestamps_opts [type: Teable.PrismaDateTime, autogenerate: {Teable.PrismaDateTime, :autogenerate, []}]

  @type t :: %__MODULE__{
          id: String.t(),
          base_id: String.t() | nil,
          space_id: String.t() | nil,
          type: String.t(),
          role: String.t(),
          invitation_code: String.t(),
          expired_time: DateTime.t() | nil,
          created_by: String.t(),
          created_time: DateTime.t(),
          last_modified_by: String.t() | nil,
          deleted_time: DateTime.t() | nil,
          updated_at: DateTime.t() | nil
        }

  schema "invitation" do
    field(:id, :string, primary_key: true)
    field(:base_id, :string)
    field(:space_id, :string)
    field(:type, :string)
    field(:role, :string)
    field(:invitation_code, :string)
    field(:expired_time, Teable.PrismaDateTime)
    field(:created_by, :string, source: :create_by)
    field(:created_time, Teable.PrismaDateTime, autogenerate: true)
    field(:last_modified_by, :string)
    field(:deleted_time, Teable.PrismaDateTime)
    timestamps(inserted_at: false, updated_at_source: :last_modified_time)
  end
end