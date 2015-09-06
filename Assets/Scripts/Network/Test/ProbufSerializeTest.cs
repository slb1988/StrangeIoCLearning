using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ProbufSerializeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {

        Tutorial.AddressBook addressbook = new Tutorial.AddressBook();

        Tutorial.Person person = new Tutorial.Person();
        person.Id = 5;
        person.Name = "sun";
        person.Phone = new List<Tutorial.Person.PhoneNumber>();
        var phoneNumItem = new Tutorial.Person.PhoneNumber();
        phoneNumItem.Number = "1862015";
        phoneNumItem.Type = Tutorial.Person.PhoneType.MOBILE;
        person.Phone.Add(phoneNumItem);
        person.Email = "sunlaibing88@gmail.com";

        addressbook.Person = new List<Tutorial.Person>();
        addressbook.Person.Add(person);

        uint serializeSize = addressbook.GetSerializedSize();

        var buffer = Tutorial.AddressBook.SerializeToBytes(addressbook);


        var address = Tutorial.AddressBook.Deserialize(buffer);

        var buffer2 = ProtobufUtil.ToByteArray(addressbook);

        var address2 = ProtobufUtil.ParseFrom<Tutorial.AddressBook>(buffer2);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
